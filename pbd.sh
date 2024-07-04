#!/bin/bash

if ./pu.sh; then # if push.sh succeeds
  cd .. # go up one directory
  rm -rf 1003-CW # remove the directory
fi # finish if statement