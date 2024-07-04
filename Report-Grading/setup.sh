#!/bin/bash

# Check if the virtual environment directory exists
VENV_DIR="venv"
if [ -d "$VENV_DIR" ]; then
  echo "Virtual environment already exists."
else
  # Create the virtual environment
  python3 -m venv $VENV_DIR
  echo "Virtual environment created."
fi

pip install --upgrade pip

# Activate the virtual environment
source $VENV_DIR/bin/activate

# Install the required packages
if [ -f "requirements.txt" ]; then
  pip install -r requirements.txt
  echo "Requirements installed."
else
  echo "requirements.txt not found."
fi



# Indicate the script has finished
echo "Setup complete. Virtual environment is ready and requirements are installed."