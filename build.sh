#!/usr/bin/env bash

set -ex

UNITY_VERSION=6000.0.0f1
GAME_CI_VERSION=3.1.0 # https://github.com/game-ci/docker/releases
MY_USERNAME=DVectors

windows() {
  GAME_CI_UNITY_EDITOR_IMAGE=$(docker pull unityci/editor:windows-$UNITY_VERSION-windows-il2cpp-$GAME_CI_VERSION)
  
}
