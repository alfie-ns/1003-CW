#!/usr/bin/env python3

import pandas as pd
import os
import matplotlib.pyplot as plt

# Provide the correct path to the Excel file
file_path = '/Users/oladeanio/Library/CloudStorage/GoogleDrive-alfienurse@gmail.com/My Drive/Uni/Undergrad/BSc (Hons) Computer Science (Artificial Intelligence) (7392)/Year_1/Semester_2/[X] COMP1003/CW/1003-CW/Report-Grading/Grading-sheet/COMP1003-Report-Marks--24.xlsx'

# Check if the file exists at the provided path
if not os.path.isfile(file_path):
    raise FileNotFoundError(f"The file was not found: {file_path}")

# Read the Excel file
df = pd.read_excel(file_path, sheet_name=0)

# Perform calculations using the 'Percent' column
your_score = 81.6  # Your actual score

mean_score = df['Percent'].mean()
median_score = df['Percent'].median()
std_dev_score = df['Percent'].std()

# Calculate the number of students who scored above and below your score
above_you = df[df['Percent'] > your_score].shape[0]
below_or_equal_you = df[df['Percent'] <= your_score].shape[0]
total_students = df.shape[0]

# Print the debug information
print(f"Debug: Scores above you: {above_you}, Scores below or equal to you: {below_or_equal_you}, Total students: {total_students}")

# Calculate your percentile rank
percentile_rank = (below_or_equal_you / total_students) * 100


# Calculate your percentile rank
percentile_rank = (below_or_equal_you / total_students) * 100

# Print the results
print(f"Mean Score: {mean_score}")
print(f"Median Score: {median_score}")
print(f"Standard Deviation: {std_dev_score}")
print(f"Number of students who scored above you: {above_you}")
print(f"Number of students who scored below or equal to you: {below_or_equal_you}")
print(f"Your percentile rank: {percentile_rank:.2f}")

# Plot histograms and other visualizations
plt.figure(figsize=(16, 8))

# Histogram of all scores
plt.subplot(2, 2, 1)
plt.hist(df['Percent'], bins=20, color='skyblue', edgecolor='black')
plt.axvline(your_score, color='r', linestyle='dashed', linewidth=2)
plt.axvline(mean_score, color='g', linestyle='dashed', linewidth=2)
plt.title('Distribution of Scores')
plt.xlabel('Scores')
plt.ylabel('Number of Students')
plt.legend(['Your Score', 'Mean Score'])

# Boxplot to show the spread of scores
plt.subplot(2, 2, 2)
plt.boxplot(df['Percent'], vert=False)
plt.axvline(your_score, color='r', linestyle='dashed', linewidth=2)
plt.title('Boxplot of Scores')
plt.xlabel('Scores')
plt.legend(['Your Score'])

# Adding a scatter plot to show individual scores
plt.subplot(2, 2, 3)
plt.scatter(range(total_students), sorted(df['Percent']), color='blue')
plt.axhline(your_score, color='r', linestyle='dashed', linewidth=2)
plt.axhline(mean_score, color='g', linestyle='dashed', linewidth=2)
plt.title('Individual Scores')
plt.xlabel('Student Index')
plt.ylabel('Score')
plt.legend(['Your Score', 'Mean Score'])

# Adding a cumulative distribution plot
plt.subplot(2, 2, 4)
sorted_scores = sorted(df['Percent'])
cumulative = [i/total_students for i in range(1, total_students+1)]
plt.plot(sorted_scores, cumulative, color='blue')
plt.axvline(your_score, color='r', linestyle='dashed', linewidth=2)
plt.title('Cumulative Distribution of Scores')
plt.xlabel('Score')
plt.ylabel('Cumulative Probability')
plt.legend(['Your Score'])

# Save the plots
plt.tight_layout()
output_dir = "/Users/oladeanio/Library/CloudStorage/GoogleDrive-alfienurse@gmail.com/My Drive/Uni/Undergrad/BSc (Hons) Computer Science (Artificial Intelligence) (7392)/Year_1/Semester_2/[X] COMP1003/CW/1003-CW/Report-Grading/Grading-sheet/Output"
plot_file_path = os.path.join(output_dir, "score_visualizations.png")
plt.savefig(plot_file_path)
plt.show()

# Ensure the output directory exists
if not os.path.exists(output_dir):
    os.makedirs(output_dir)

# Save the cleaned DataFrame to an Excel file
output_file_path = os.path.join(output_dir, "cleaned_data.xlsx")
df.to_excel(output_file_path, index=False)

print("Data and plots saved successfully to", output_dir)