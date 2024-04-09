![Keepr Preview Image](KeeprScreenshot.png)

# Keepr

Keepr is a Pinterest style full-stack web application which allows users to visually share and explore new interests by adding images to personal collections.

[See Keepr live hosted here](https://keepr.corinnabolon.com)

## Technologies Used:
• Vue.js with Vue-Router for front-end development

• DotNet WebApi for server-side operations

• Auth0 integration for secure user management

• Dapper as the ORM for database interactions with MySQL

## Key Features:
Vaults and Keeps: Users can create custom Vaults, thematic collections that house their Keeps (images). This unique feature enables users to curate and share their interests in an organized manner.

Privacy Settings: Vaults can be marked as private, offering a personalized and secure space for users. Only the creator has access to private Vaults, ensuring a more intimate and personalized experience.

View and Save Count: Keepr acknowledges and displays the popularity of Keeps by showing view counts. Additionally, the 'Kept' count increments each time a user adds a Keep to their Vault, providing recognition for engaging content.

User Profiles: Every user has a public profile page, showcasing their Keeps and Vaults. This adds a social dimension, allowing users to explore and connect based on shared interests.

## What I learned from this project:
Through the development of Keepr, I deepened my understanding of C# and honed my skills in working with the .NET Framework. Specifically, implementing and optimizing the handling of data relationships was a significant learning point, as well as devising an efficient system for managing view and kept counts on the back end using C#. I continued to hone my front-end skills with Vue.js, and am particularly proud of the masonry layout of the pictures. I also tackled the challenge of hosting a MySQL database on AWS, touching on the intricacies of database management and connectivity within the application.

## Installing Dependencies / Build:

<b>Client</b>
Node.js is required to run the project. If you haven't installed Node.js yet, you can download it from <a href="https://nodejs.org/en">the official Node.js website</a>.

-Navigate to the client directory.
-Run <b>npm install</b> to install dependencies for the client side.
-Run <b>npm run build</b> to build the client-side application.

<b>Server</b>
The .NET SDK is required to build and run the server-side project. You can download and install it from <a href="https://dotnet.microsoft.com/en-us/download">the official .NET website</a>.

-Navigate to the server directory.
-Run <b>dotnet restore</b> to install dependencies for the server side.

## Docker Deployment:

<b>Installing Docker</b>

Windows:
Download the Docker Desktop installer from the official Docker website and follow the installation instructions.
Once installed, Docker Desktop should be running, and you can access it from the system tray.
macOS:
Download the Docker Desktop installer from the official Docker website and follow the installation instructions.
Once installed, Docker Desktop should be running, and you can access it from the menu bar.
Linux:
Follow the installation instructions for Docker on your specific Linux distribution. You can find installation instructions on the Docker documentation website.

After installing Docker, ensure it's running and accessible from the command line by running <b>docker --version</b>.

<b>Building and Running Docker Container</b>
-Navigate to the root directory of your project.
-Run <b>docker build -t your-image-name</b> to build the Docker image. (Replace your-image-name with the desired name for your Docker image.)
-Once the image is built, you can run your container using <b>docker run -p 8080:7045 your-image-name</b>.

Navigate to http://localhost:8080/ to see the running app!
