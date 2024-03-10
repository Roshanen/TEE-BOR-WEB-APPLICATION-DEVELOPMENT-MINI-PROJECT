# TEE-BOR-WEB-APPLICATION-DEVELOPMENT-MINI-PROJECT

Web Application Development Mini Project using ASP.NET MVC.

This app helps bring people together for events.

<!-- Build -->
## Building a sample

Build any .NET Core sample using the .NET Core CLI, which is installed with [the .NET Core SDK](https://www.microsoft.com/net/download). Then run
these commands from the CLI in the directory of any sample:

```console
dotnet build
dotnet run
```

<!-- Dev -->
## Dev Mode

In development process we could use `dotnet watch`.
The dotnet watch command is a file watcher. When it detects a change, it runs the dotnet run command or a specified dotnet command.

```console
dotnet watch
```

<!-- ENV -->
## .env Setup

Store your sensitive Data about Connections in .env file.

```
MONGODB_CONNECTION_STRING="your mongodb connection string"
DATABASE_NAME="your database name"
JWT_SECRET_KEY="your jwt secret key"
```

_Last thing is "this got me so sleepy"._