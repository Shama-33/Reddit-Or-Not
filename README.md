# Reddit-Or-Not - A Reddit-like ASP.NET Web Application

A Reddit-like community platform built with ASP.NET Web Forms and Microsoft SQL Server. Users can create communities, share posts, comment, and vote on content.


## Features

- User registration and authentication
- Create and join communities
- Post creation (text, images)
- Commenting system
- Upvote/downvote functionality
- User profiles 
- Search functionality
- Community management
- Notifications

## Requirements

- Visual Studio 2019 or later
- Microsoft SQL Server 2016+ (or SQL Server Express)
- SQL Server Management Studio (SSMS)
- .NET Framework 4.7.2 or later
- Windows 10/11

## Installation

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/reddit-clone-aspnet.git
```

### 2. Open in Visual Studio
- Open Visual Studio
- File → Open → Project/Solution
- Select the `.sln` file

### 3. Restore NuGet Packages
Visual Studio will automatically restore packages. If not:
- Right-click Solution → Restore NuGet Packages

### 4. Setup Database

#### Create Database in SSMS:
1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Create new database named `RedditCloneDB`


#### Update Connection String:
Open `Web.config` and update the connection string:

```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=RedditCloneDB;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Common server names:**
- `localhost` or `.` - Local SQL Server
- `.\SQLEXPRESS` - SQL Server Express
- `(localdb)\MSSQLLocalDB` - LocalDB

### 5. Run the Application
- Press `F5` or click Start
- Application opens at `http://localhost:[port]/`

## Usage

### Register/Login
1. Click "Sign Up" to create an account
2. Login with your credentials

### Create a Community
1. Click "Create Community"
2. Enter community name
3. Submit

### Create a Post
1. Select a community
2. Click "Create Post"

### Comment and Vote
- Click on any post to view and comment
- Use arrows to upvote/downvote

## Project Structure

```
RedditClone/
├── Avatars/              # User avatar uploads
├── CommunityImages/      # Community images
├── CSS/                  # Stylesheets
├── Images/               # Static images
├── Login.aspx            # Login page
├── Registration.aspx     # Registration page
├── CommunityPage.aspx    # Community view
├── PostDetails.aspx      # Post details
├── Profile.aspx          # User profile
├── Web.config            # Configuration
└── *.cs                  # Code-behind files
```

## Database Tables

Main tables:
- **Users** - User accounts
- **Communities** - Community information
- **Posts** - User posts
- **Comments** - Post comments
- **CommunityMembers** - Community memberships
- **Votes** - User votes

## Technologies

- **Backend**: ASP.NET Web Forms, C#
- **Database**: Microsoft SQL Server
- **Frontend**: HTML, CSS, JavaScript, Bootstrap
- **Authentication**: ASP.NET Forms Authentication

## Troubleshooting

### Database Connection Failed
- Check SQL Server is running (Services → SQL Server)
- Verify connection string in Web.config
- Test connection in SSMS first

### Login Issues
- Clear browser cache and cookies
- Check database has Users table with data
- Verify Web.config authentication settings

### Images Not Uploading
- Check folder permissions for Avatars and CommunityImages
- Verify file size limits in Web.config

## License

This project is open source and available under the MIT License.



⭐ If you find this project useful, please give it a star!
# Reddit-Or-Not
