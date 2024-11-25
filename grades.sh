#!/bin/bash
cd Report/Feedback/grade-analysis
./setup.sh
./grading-sheet.py
sleep 1
echo -e "\n cd into the 'Report-Grading' directory"
echo -e "\n'source venv/bin/activate' to activate the virtual environment"
echo -e "\nThen './grading-sheet.py'"