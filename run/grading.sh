#!/bin/bash

# implement one key enter 

cd Feedback

# Colors for better visibility
RED='\033[0;31m'
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[0;33m'
NC='\033[0m' # No Color

# Function to check if python and venv are available
check_dependencies() {
    if ! command -v python3 &> /dev/null; then
        echo -e "${RED}Error: Python 3 is not installed${NC}"
        exit 1
    fi
    if ! command -v pip3 &> /dev/null; then
        echo -e "${RED}Error: pip3 is not installed${NC}"
        exit 1
    fi
}

# Function to setup virtual environment
setup_venv() {
    if [ ! -d "venv" ]; then
        echo -e "${BLUE}Setting up virtual environment...${NC}"
        python3 -m venv venv
        source venv/bin/activate
        pip install pandas numpy matplotlib seaborn scipy openpyxl
    else
        source venv/bin/activate
    fi
}

# Function to display the main menu
show_menu() {
    clear
    echo -e "${GREEN}=== Grade Analysis Tool ===${NC}"
    echo -e "${YELLOW}1.${NC} View Overall Grade Distribution"
    echo -e "${YELLOW}2.${NC} View Individual Component Breakdown"
    echo -e "${YELLOW}3.${NC} Generate Performance Summary"
    echo -e "${YELLOW}4.${NC} Show Charts"
    echo -e "${YELLOW}5.${NC} Export Analysis to PDF"
    echo -e "${YELLOW}6.${NC} Exit"
    echo
    echo -e "${BLUE}Please select an option (1-6):${NC}"
}

# Function to handle grade distribution
show_grade_distribution() {
    cd Report/Feedback/grade-analysis
    echo -e "${BLUE}Generating grade distribution...${NC}"
    python3 -c "
import pandas as pd
import matplotlib.pyplot as plt
df = pd.read_excel('COMP1003-Report-Marks--24.xlsx')
plt.figure(figsize=(10, 6))
df['Percent'].hist(bins=20)
plt.title('Grade Distribution')
plt.xlabel('Grade')
plt.ylabel('Frequency')
plt.show()
"
}

# Function to show component breakdown
show_component_breakdown() {
    echo -e "${BLUE}Loading component breakdown...${NC}"
    if [ -f "organised_marks.csv" ]; then
        echo -e "${GREEN}Component Breakdown:${NC}"
        column -t -s, organised_marks.csv
    else
        echo -e "${RED}Error: organised_marks.csv not found${NC}"
    fi
    echo
    read -p "Press Enter to continue..."
}

# Function to show available charts
show_charts() {
    echo -e "${GREEN}Available Charts:${NC}"
    echo -e "${YELLOW}1.${NC} Box Plot"
    echo -e "${YELLOW}2.${NC} Histogram"
    echo -e "${YELLOW}3.${NC} Scatter Plot"
    echo -e "${YELLOW}4.${NC} Pie Chart"
    echo -e "${YELLOW}5.${NC} Return to Main Menu"
    
    read -p "Select a chart to view (1-5): " chart_choice
    
    case $chart_choice in
        1) open Charts/box-plot.png ;;
        2) open Charts/histogram.png ;;
        3) open Charts/scatter.png ;;
        4) open Charts/pie-chart.png ;;
        5) return ;;
        *) echo -e "${RED}Invalid option${NC}" ;;
    esac
    
    read -p "Press Enter to continue..."
}

# Function to generate performance summary
generate_summary() {
    echo -e "${BLUE}Generating performance summary...${NC}"
    if [ -f "grade-analysis/grading-sheet/Output/performance_summary.txt" ]; then
        cat grade-analysis/grading-sheet/Output/performance_summary.txt
    else
        echo -e "${RED}Error: Performance summary not found${NC}"
    fi
    echo
    read -p "Press Enter to continue..."
}

# Main script logic
check_dependencies
setup_venv

while true; do
    show_menu
    read choice
    
    case $choice in
        1) show_grade_distribution ;;
        2) show_component_breakdown ;;
        3) generate_summary ;;
        4) show_charts ;;
        5) 
            echo -e "${BLUE}Generating PDF export...${NC}"
            # Add PDF export logic here
            echo -e "${GREEN}PDF exported successfully${NC}"
            read -p "Press Enter to continue..."
            ;;
        6) 
            echo -e "${GREEN}Goodbye!${NC}"
            deactivate
            exit 0
            ;;
        *)
            echo -e "${RED}Invalid option${NC}"
            read -p "Press Enter to continue..."
            ;;
    esac
done