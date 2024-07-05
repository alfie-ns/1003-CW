#!/bin/bash

# Check if the virtual environment directory exists
VENV_DIR="venv"
if [ -d "$VENV_DIR" ]; then
  echo "Virtual environment already exists."
else
  # Create the virtual environment
  python3 -m venv $VENV_DIR
  echo -e "\nVirtual environment created."
fi

# Activate the virtual environment
source $VENV_DIR/bin/activate

# Install the required packages
if [ -f "requirements.txt" ]; then
  pip install -r requirements.txt
  echo -e "\nRequirements installed."
else
  echo "requirements.txt not found."
fi

pip install --upgrade pip




# Indicate the script has finished
echo -e "\nSetup complete. Virtual environment is ready and requirements are installed."

cd Report-Grading