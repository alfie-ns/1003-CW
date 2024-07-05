#!/usr/bin/env python3

import pandas as pd
import numpy as np
import os
import matplotlib.pyplot as plt
import seaborn as sns
from scipy import stats

# File path (unchanged)
file_path = '/Users/oladeanio/Library/CloudStorage/GoogleDrive-alfienurse@gmail.com/My Drive/Uni/Undergrad/BSc (Hons) Computer Science (Artificial Intelligence) (7392)/Year_1/Semester_2/[X] COMP1003/CW/1003-CW/Report-Grading/Grading-sheet/COMP1003-Report-Marks--24.xlsx'

# File existence check (unchanged)
if not os.path.isfile(file_path):
    raise FileNotFoundError(f"The file was not found: {file_path}")

# Read the Excel file
df = pd.read_excel(file_path, sheet_name=0)

# Your score and identifier
your_score = 81.6
your_identifier = "Participant 13126631"

# Basic statistics
mean_score = df['Percent'].mean()
median_score = df['Percent'].median()
std_dev_score = df['Percent'].std()
min_score = df['Percent'].min()
max_score = df['Percent'].max()

# Calculate percentile rank and z-score
percentile_rank = stats.percentileofscore(df['Percent'], your_score)
z_score = (your_score - mean_score) / std_dev_score

# Calculate your rank
df_sorted = df.sort_values('Percent', ascending=False)
your_rank = df_sorted['Percent'].rank(method='min', ascending=False)[df_sorted['Identifier'] == your_identifier].values[0]

# Calculate number of students above and below your score
students_above = df[df['Percent'] > your_score].shape[0]
students_below = df[df['Percent'] < your_score].shape[0]

# Print detailed analysis
print(f"Your Score: {your_score:.2f}")
print(f"Class Mean: {mean_score:.2f}")
print(f"Class Median: {median_score:.2f}")
print(f"Class Standard Deviation: {std_dev_score:.2f}")
print(f"Minimum Score: {min_score:.2f}")
print(f"Maximum Score: {max_score:.2f}")
print(f"Your Percentile Rank: {percentile_rank:.2f}")
print(f"Your Z-Score: {z_score:.2f}")
print(f"Your Rank: {your_rank:.0f} out of {df.shape[0]}")
print(f"Students who scored higher than you: {students_above}")
print(f"Students who scored lower than you: {students_below}")

# Calculate performance metrics
performance_summary = f"""
Performance Summary:
--------------------
1. You scored in the top {100 - percentile_rank:.2f}% of the class.
2. Your score is {z_score:.2f} standard deviations above the mean.
3. You outperformed {students_below} students and were outperformed by {students_above} students.
4. Your score of {your_score:.2f} is {your_score - mean_score:.2f} points above the class average.
5. You are ranked {your_rank:.0f} out of {df.shape[0]} students.
"""

print(performance_summary)

# Enhanced visualizations
plt.figure(figsize=(20, 16))

# Histogram with KDE
plt.subplot(2, 2, 1)
sns.histplot(df['Percent'], kde=True, color='skyblue')
plt.axvline(your_score, color='r', linestyle='dashed', linewidth=2, label='Your Score')
plt.axvline(mean_score, color='g', linestyle='dashed', linewidth=2, label='Mean Score')
plt.title('Distribution of Scores with KDE')
plt.xlabel('Scores')
plt.ylabel('Density')
plt.legend()

# Box plot with individual points
plt.subplot(2, 2, 2)
sns.boxenplot(x=df['Percent'], color='lightgreen')
sns.stripplot(x=df['Percent'], color='blue', alpha=0.5)
plt.axvline(your_score, color='r', linestyle='dashed', linewidth=2, label='Your Score')
plt.title('Boxenplot with Individual Scores')
plt.xlabel('Scores')
plt.legend()

# Cumulative distribution plot
plt.subplot(2, 2, 3)
sorted_scores = np.sort(df['Percent'])
cumulative = np.arange(1, len(sorted_scores) + 1) / len(sorted_scores)
plt.plot(sorted_scores, cumulative, color='purple')
plt.axvline(your_score, color='r', linestyle='dashed', linewidth=2, label='Your Score')
plt.title('Cumulative Distribution of Scores')
plt.xlabel('Score')
plt.ylabel('Cumulative Probability')
plt.legend()

# Q-Q plot
plt.subplot(2, 2, 4)
stats.probplot(df['Percent'], dist="norm", plot=plt)
plt.title('Q-Q Plot of Scores')

# Save the plots
plt.tight_layout()
output_dir = "/Users/oladeanio/Library/CloudStorage/GoogleDrive-alfienurse@gmail.com/My Drive/Uni/Undergrad/BSc (Hons) Computer Science (Artificial Intelligence) (7392)/Year_1/Semester_2/[X] COMP1003/CW/1003-CW/Report-Grading/Grading-sheet/Output"
plot_file_path = os.path.join(output_dir, "enhanced_score_visualizations.png")
plt.savefig(plot_file_path)
plt.show()

# Ensure the output directory exists (unchanged)
if not os.path.exists(output_dir):
    os.makedirs(output_dir)

# Save the performance summary to a text file
summary_file_path = os.path.join(output_dir, "performance_summary.txt")
with open(summary_file_path, 'w') as f:
    f.write(performance_summary)

print(f"Enhanced data analysis, plots, and performance summary saved successfully to {output_dir}")