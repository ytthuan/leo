Contents
I.	Setup sample project to remote debug	2
1.	From windows open command windows	2
2.	Copy the content of file	2
3.	Ssh to linux	3
4.	Follow the below link to install dotnet cli	3
5.	Install unzip	3
6.	Install VSDBG by running the following command. Replace '~/vsdbg' to wherever you want vsdbg installed.	3
7.	Download plink.exe	4
8.	Save file to c:\mytools\	4
9.	Open putty key generator download from the same page above	4
10.	Test connection	4
11.	Open Sample project, In the launch.json from customer project	5
12.	In task.json	5
II.	Configure template projects to demonstrate remote debugging with HttpClient	6
1.	From Linux Create two dirs for publishing in linux	7
2.	From Windows or the environment hosting VS Code create two projects by command in VS Code terminal	7
3.	Generate debug asset files for both projects.	7
4.	In leoApi project config task.json make sure the below task should be the same, leave the rest in default	7
5.	In project leoConsumeApi	10
6.	From project leoApi select Leo remote debugging and F5 to start debugging	12
7.	From project leoConsumeApi select remote debugger set breakpoint and F5 to start debugging	13

 
I.	Setup sample project to remote debug
Follow the below article to create SSH key pair.
https://docs.microsoft.com/en-us/azure/virtual-machines/linux/ssh-from-windows
 
1.	From windows open command windows
 
C:\WINDOWS\system32>ssh-keygen -t rsa -b 2048
Generating public/private rsa key pair.
Enter file in which to save the key (C:\Users\ytthu/.ssh/id_rsa):
Enter passphrase (empty for no passphrase):
Enter same passphrase again:
Your identification has been saved in C:\Users\ytthu/.ssh/id_rsa.
Your public key has been saved in C:\Users\ytthu/.ssh/id_rsa.pub.
The key fingerprint is:
SHA256:mmErwpylGHnocIN2T22hWHPWR9dI+i7hZj5nag3l9Kg ytthu@ThuanLy
The key's randomart image is:
+---[RSA 2048]----+
|           ..oo  |
|        . . o. . |
|     o + . o     |
| +  o * . . .o   |
|*.+o.oooS  .+.o  |
|+B.=o..=  ..oo . |
|..* ..+    =+.   |
|   . .    +E.+   |
|          .o=    |
+----[SHA256]-----+
 
Leave the passphase empty
 
2.	Copy the content of file 
 
C:\Users\ytthu/.ssh/id_rsa.pub
 
And pass into the linux vm - reset ssh public key
--> update ssh
 
 
 
 
3.	Ssh to linux
 
 
 
Or 
 
 
4.	Follow the below link to install dotnet cli
 
https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install
 
5.	Install unzip
 
$ sudo apt-get install unzip
 
6.	Install VSDBG by running the following command. Replace '~/vsdbg' to wherever you want vsdbg installed.
wget https://aka.ms/getvsdbgsh -O - 2>/dev/null | /bin/sh /dev/stdin -v latest -l ~/vsdbg
 
Using arguments
    Version                    : 'latest'
    Location                   : '/home/ytthu/vsdbg'
    SkipDownloads              : 'false'
    LaunchVsDbgAfter           : 'false'
    RemoveExistingOnUpgrade    : 'false'
Info: Using vsdbg version '16.0.20322.2'
Info: Previous installation at '/home/ytthu/vsdbg' not found
Info: Using Runtime ID 'linux-x64'
Downloading https://vsdebugger.azureedge.net/vsdbg-16-0-20322-2/vsdbg-linux-x64.zip
Info: Successfully installed vsdbg at '/home/ytthu/vsdbg'
 
 
7.	Download plink.exe
 
https://www.chiark.greenend.org.uk/~sgtatham/putty/latest.html
 
 
8.	Save file to c:\mytools\
9.	Open putty key generator download from the same page above
Load key --> select private key from C:\Users\ytthu\.ssh
Save private key to the same folder named as puttysshconverted.ppk
 
 
 
If you don’t do this step you will be failed when use file id_rsa for authen you will get below error
 
 
 
10.	Test connection
C:\WINDOWS\system32>C:\mytools\plink.exe -i C:\Users\ytthu\.ssh\puttysshconverted.ppk ytthu@linuxremotedebug.eastasia.cloudapp.azure.com -batch -T echo "Hello world"
 
Output
 
 
 
You are ready to remote debug with VSCode
 
11.	Open Sample project, In the launch.json from customer project
 `
{
"name": "LEO .NET Core Remote Linux",
"type": "coreclr",
"request": "launch",
"program": "app.dll",
"cwd": "/home/ytthu/sample/", -- the published folder that project will be published.
"stopAtEntry": false,
"console": "internalConsole",
"pipeTransport": {
"pipeCwd": "${workspaceFolder}",
"pipeProgram": "C:\\mytools\\plink.exe ", 
"pipeArgs": [
"ytthu@linuxremotedebug.eastasia.cloudapp.azure.com","-i", "C:\\Users\\ytthu\\.ssh\\puttysshconverted.ppk",
"-batch", "-T"
],
"debuggerPath": "~/vsdbg/vsdbg"
},
"preLaunchTask": "deploy"
}
 
 
12.	In task.json
 


{
"label": "deploy",
"command": "scp",
"type": "shell",
"args": [
"C:\\Projects\\sample\\sample\\app\\bin\\Debug\\netcoreapp2.2\\publish\\*",
"ytthu@linuxremotedebug.eastasia.cloudapp.azure.com:/home/ytthu/sample"
],
"dependsOn": ["publish"]
},
 `
Make sure created folder by command: mkdir /home/ytthu/sample
 
"C:\\Projects\\sample\\sample\\app\\bin\\Debug\\netcoreapp2.2\\publish\\*", is the published folder in windows path
 
 
 
Then f5 and enjoy
 

II.	Configure template projects to demonstrate remote debugging with HttpClient

Assumed to demonstrate the remote debug with remote linux, VS code C# .NET core, at the moment we try the simplest web service use HttpClient in System.Net library

Create two projects
-	Web service project named leoApi – as stand for module A, this web service will listen http request from linux debug machine


-	Consume service project named leoConsumeApi
Simply to send a http GET request to leoApi web service to get data from. 


1.	From Linux Create two dirs for publishing in linux
a.	Mkdir  /home/admin2019/leoconsumeapi
b.	Mkdir /home/admin2019/leoapi
NOTE: linux naming convention is case sensitive

2.	From Windows or the environment hosting VS Code create two projects by command in VS Code terminal

#create web api template
dotnet new webapi -o leoApi
#create console app template
dotnet new console -o leoConsumeApi

3.	Generate debug asset files for both projects.
4.	In leoApi project config task.json make sure the below task should be the same, leave the rest in default 
`
{
            "label": "publish",
            "command": "dotnet",
            "type": "process",
            "args": [
                "publish",
                "${workspaceFolder}/leoApi.csproj"
            ],
            "problemMatcher": "$tsc"
        },

        {
            "label": "deploy",
            "command": "scp",
            "type": "shell",
            "args": [
                "${workspaceFolder}/bin/Debug/netcoreapp2.2/publish/*",
                "admin2019@linuxremotedebug.eastasia.cloudapp.azure.com:/home/admin2019/leoapi"
            ],
            "dependsOn": [
                "publish"
            ]
        },

Launch.json add the remote debugger

  {
            "name": "Leo .NET Core Remote Linux",
            "type": "coreclr",
            "request": "launch",
            "program": "leoApi.dll",
            "cwd": "/home/admin2019/leoapi/",
            "stopAtEntry": false,
            "console": "internalConsole",
            "pipeTransport": {
                "pipeCwd": "${workspaceFolder}",
                "pipeProgram": "C:\\mytools\\plink.exe",
                "pipeArgs": [
                    "admin2019@linuxremotedebug.eastasia.cloudapp.azure.com",
                    "-i",
                    "C:\\Users\\admin2019\\.ssh\\puttysshconverted.ppk",
                    "-batch",
                    "-T"
                ],
                "debuggerPath": "/home/ytthu/vsdbg/vsdbg"
            },

            "env": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "sourceFileMap": {
                "/Views": "${workspaceFolder}/Views"
            },
            "preLaunchTask": "deploy"
        }
`
Main method in program.cs
`
public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                      .UseKestrel()
                      .UseStartup<Startup>()
                      .UseIISIntegration()
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .Build();
            host.Run();

            //CreateWebHostBuilder(args).Build().Run();
        }
`
In leoApi.csproj add IISIntegration as below
`<ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.AspNetCore.Razor.Design" Version="2.2.0" PrivateAssets="All" />
    <PackageReference Include="Microsoft.AspNetCore.Server.IISIntegration" Version="2.2.0" />
  </ItemGroup>`

In launchSettings.json in properties branch
 
Note the port 
 

5.	In project leoConsumeApi

In launch.json file add below debugger
`
{
            "name": "Leo .NET Core Remote Linux",
            "type": "coreclr",
            "request": "launch",
            "program": "leoConsumeApi.dll",
            "cwd": "/home/admin2019/leoconsumeapi/",
            "stopAtEntry": false,
            "console": "internalConsole",
            "pipeTransport": {
                "pipeCwd": "${workspaceFolder}",
                "pipeProgram": "C:\\mytools\\plink.exe",
                "pipeArgs": [
                    "admin2019@linuxremotedebug.eastasia.cloudapp.azure.com",
                    "-i",
                    "C:\\Users\\admin2019\\.ssh\\puttysshconverted.ppk",
                    "-batch",
                    "-T"
                ],
                "debuggerPath": "/home/ytthu/vsdbg/vsdbg"
            },           
            "preLaunchTask": "deploy"
        }
`
In task.json

Make sure modify the below tasks
`{
            "label": "publish",
            "command": "dotnet",
            "type": "process",
            "args": [
                "publish",
                "${workspaceFolder}/leoConsumeApi.csproj"
            ],
            "problemMatcher": "$tsc"
        },
        {
            "label": "deploy",
            "command": "scp",
            "type": "shell",
            "args": [
                "${workspaceFolder}/bin/Debug/netcoreapp2.2/publish/*",
                "admin2019@linuxremotedebug.eastasia.cloudapp.azure.com:/home/admin2019/leoconsumeapi"
            ],
            "dependsOn": [
                "publish"
            ]
        },

Add the following code in main method program.cs

class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           string url = "http://localhost:5000/api/values";
           HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,url);
           HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();
           Console.WriteLine($"output: {response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
           Console.ReadLine();
        }
    }
`
6.	From project leoApi select Leo remote debugging and F5 to start debugging 
The app url is http://localhost:5000
Localhost now is meaning linux debug machine
 
Set breakpoint in ValuesController.cs
 

From ssh console, linux machine is now listening on port 5000
 
7.	From project leoConsumeApi select remote debugger set breakpoint and F5 to start debugging 
The localhost here is treated as linux remote machine as we are remotely debugging on that machine.
 
Debugger hit breakpoint
   

 

When debugger run into line 14 – send HttpRequest the breakpoint from project leoApi will be hit.


 

F10 to continue to end the method GET and debugger will back to leoConsumeApi

 
F10 to finish, and see the output.
 

--END--
