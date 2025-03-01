# Sharing Games API

Explore a large amount of games and learn more about them: their description, their publisher, when they were released, their genres and much more. Favorite them and share with your friends! Didn't find what you were looking? Submit a feedback or a game request!

## Features
- User authentication;
- Get a single game genre or a list of game genres;
- Get a single game or a list of games;
- Users can favorite games and submit feedbacks/game requests.

## What I've learned
- Use of ASP.NET Identity Framework to handle user authentication;
- Use of the new implicit operators to perform conversions between DTOs and entities classes;
- Model validations using FluentValidation;
- Rate limiting the number of requests;
- Integration tests to guarantee the system's reliability and quality;
- Integration of Github Actions, using the results of the integration tests to block or allow pull requests approval;

## Running locally
 - Dotnet 8.0 and PostgreSQL are required;
 - Fill up your your environment file with your postgres database url, in the `DATABASE_CONNECTION_STRING` key;
 - Build the solution with `dotnet build` command;
 - Create your database and run the migrations with the `dotnet ef database update --project src/Infra --startup-project src/App` command;
 - Run the tests with the `dotnet test` command or run the project and explore the endpoints with the `dotnet run --project src/App` command.

## Next steps (may never be taken)
- [ ] Integrate the main functionalities in a client app, including administration side;