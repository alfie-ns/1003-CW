#!/bin/bash

if ./pu.sh; then # if push.sh succeeds
  cd .. # go up one directory
  sleep 3 # wait for 3 seconds
  rm -rf 1003-CW # remove the directory
fi # finish if statement