using System;
using System.Collections.Generic;
using System.IO;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

namespace Bloodshed.Config
{
    public class FileWatcher
    {
        public static BloodshedModSystem Bloodshed => BloodshedModSystem.Instance;

        private DateTime lastChange = DateTime.MinValue;
        private readonly List<FileSystemWatcher> watchers = new();

        public bool Queued { get; set; }

        public FileWatcher()
        {
            var paths = new[]
            {
                (GamePaths.ModConfig, $"{Bloodshed.ModId}.json", false),
                //(Path.Combine(GamePaths.ModConfig, $"{Bloodshed.ModId}", "recipes"), "*.json", true)
            };

            foreach (var (path, filter, scanSubDir) in paths)
            {
                if (!Directory.Exists(path)) continue;
                var watcher = new FileSystemWatcher(path)
                {
                    Filter = filter,
                    IncludeSubdirectories = scanSubDir,
                    EnableRaisingEvents = true
                };

                watcher.Changed += Changed;
                watcher.Created += Changed;
                watcher.Deleted += Changed;
                watcher.Renamed += Changed;
                watcher.Error += Error;

                watchers.Add(watcher);
            }
        }

        private void Changed(object sender, FileSystemEventArgs e)
        {
            // Debounce chcnages
            var now = DateTime.UtcNow;
            if ((now - lastChange).TotalMilliseconds < 200) return;

            lastChange = now;
            Bloodshed.Api.Event.EnqueueMainThreadTask(() => QueueReload(true), "queueReload");
        }

        private void Error(object sender, ErrorEventArgs e)
        {
            Bloodshed.Logger.Error(e.GetException().ToString());
            Bloodshed.Api.Event.EnqueueMainThreadTask(() => QueueReload(), "queueReload");
        }

        /// <summary>
        /// Workaround for <a href='https://github.com/dotnet/runtime/issues/24079'>dotnet#24079</a>.
        /// </summary>
        private void QueueReload(bool changed = false)
        {
            // Check if already queued for reload
            if (Queued) return;

            // Mark as queued
            Queued = true;

            // Inform console/log
            if (changed) Bloodshed.Logger.Event($"Detected {Bloodshed.ModId} config was changed, reloading.");

            // Wait for other changes to process
            Bloodshed.Api.Event.RegisterCallback(_ => {
                // Reload the config
                Bloodshed.ReloadConfig(Bloodshed.Api, true);

                // Wait some more to remove this change from the queue since the reload triggers another write
                Bloodshed.Api.Event.RegisterCallback(_ => {
                    // Unmark as queued
                    Queued = false;
                }, 100);
            }, 100);
        }

        public void Dispose()
        {
            foreach (var watcher in watchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Changed -= Changed;
                watcher.Created -= Changed;
                watcher.Deleted -= Changed;
                watcher.Renamed -= Changed;
                watcher.Error -= Error;
                watcher.Dispose();
            }

            watchers.Clear();
        }
    }
}
