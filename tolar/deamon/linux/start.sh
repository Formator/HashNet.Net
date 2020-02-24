#!/bin/sh

cd /daemon
echo "Starting Tolar HashNet SiChain Daemon"
./tolar_daemon_linux --log_dir=/.tolar/logs --config_path=config.json