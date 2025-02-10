using System;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace PApp;

public partial class ProfilePage : ContentPage
{
    private const string ProfileFileName = "profile.json";
    private string ProfileFilePath => Path.Combine(FileSystem.AppDataDirectory, ProfileFileName);

    public ProfilePage()
    {
        InitializeComponent();
        LoadProfile();
    }

    // Method to load profile data from a JSON file
    private void LoadProfile()
    {
        try
        {
            // Check if the file exists
            if (File.Exists(ProfileFilePath))
            {
                var json = File.ReadAllText(ProfileFilePath);
                var profile = JsonSerializer.Deserialize<UserProfile>(json);
                if (profile != null)
                {
                    nameEntry.Text = profile.Name;
                    surnameEntry.Text = profile.Surname;
                    emailEntry.Text = profile.Email;
                    bioEditor.Text = profile.Bio;
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any potential errors (e.g., file not found, invalid JSON, etc.)
            Console.WriteLine($"Error loading profile: {ex.Message}");
        }
    }

    // Method to save the updated profile to a JSON file
    private void SaveProfile()
    {
        try
        {
            var profile = new UserProfile
            {
                Name = nameEntry.Text,
                Surname = surnameEntry.Text,
                Email = emailEntry.Text,
                Bio = bioEditor.Text
            };

            var json = JsonSerializer.Serialize(profile);
            File.WriteAllText(ProfileFilePath, json);
            DisplayAlert("Success", "Profile saved successfully", "OK");
        }
        catch (Exception ex)
        {
            // Handle any potential errors (e.g., file write issues)
            Console.WriteLine($"Error saving profile: {ex.Message}");
            DisplayAlert("Error", "Failed to save profile", "OK");
        }
    }

    // Method to handle save button click
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        SaveProfile();
    }
}

// Profile class to represent user data
public class UserProfile
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
}
