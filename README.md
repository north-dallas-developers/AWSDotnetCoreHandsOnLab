# AWS Dotnet Core Hands-On Lab

Here's the basic walkthrough.

## Prerequisites

* .net core 3.1.
* You need to have an AWS account.
* Sql Management Studio/Azure Data Studio/something to connect to Sql Server with to run queries.

If using Windows and want to deploy to Linux, install WinSCP (https://winscp.net/eng/index.php).

## Step 1: Get the code

* Download this: https://github.com/north-dallas-developers/AWSDotnetCoreHandsOnLab
* Run it to make sure it works, then stop it

## Step 2: Code Review

Let's look at the code

## Step 3: Create RDS Database

This is done first because databases take the longest to created.

* Create Sql Server database
    - Standard create
    - Express 2017
    - Free Tier
    - Name: `devdb`
* Write down whatever credentials you choose
* Make it publicly accessible

## Step 4: Publish a self-contained app

`dotnet publish -c Release -r linux-x64 --self-contained true`

See https://docs.microsoft.com/en-us/dotnet/core/rid-catalog for other runtime identifiers other than linux-x64.

## Step etc.: Zip the files up

Zip: `zip -r app_files.zip bin/Release/netcoreapp3.1/linux-x64/publish/`
Tar: `tar -czvf app_files.tar.gz -C bin/Release/netcoreapp3.1/linux-x64/publish/ .`

## Into to VPCs, Subnets, and EC2

Explain VPCs, subnets, and EC2
Open up VPC in the console
Show pinning

## Create EC2 Instance

Create an EC2 instance.

* Search for "ubuntu"
* Choose the top option, the free tier eligible Ubuntu 18.04
* Go through the dialog until you get the key pair dialog
* Create a new key pair named "nddg-keypair". Download new key pair called `nddg-keypair.pem`. Save it to a file and put it (for now) in the directory with your code.
* Start instance (should take about 30 seconds to create)

Re-explain EC2, VPCs, subnets with what they have.

Name your security groups

## Get Code to the Server

Presenter note: First show bash version, then winscp version.

Necessary before using `scp` and `ssh` below.

`chmod 400 feb-fun.pem`

Now ssh into the box to make sure you can connect. Then `exit`.

`ssh -i nddg-keypair.pem ubuntu@[servername]`

To copy your files to the server:

`scp -i nddg-keypair.pem app_files.zip ubuntu@[servername]:~/.`

`scp -i nddg-keypair.pem app_files.tar.gz ubuntu@[servername]:~/.`

Ssh into the box again. To install unzip if you used zip, untar if you used tar. If you used zip, you probably need to install it.

`sudo apt install unzip`

Create a directory for the app.

`mkdir app`

Now untar/unzip the code.

Zip: `unzip app_files.zip`
Tar: `tar -zxvf app_files.tar.gz -C app`

## Running the App

Start the app by `cd`ing into the directory and running this:

`./AWSDotnetCoreHandsOnLab --urls http://+:5000 --environment Development`

Add rule to open port 5000 (launch-wizard-*x*)

Find the server url and go there in the browser.

## Setup the Database

* Add rule allowing 1433 traffic for your ip, source `0.0.0.0/0`
* Connect via SSMS or Azure Data Studio
* Run `database-script.sql`
* Change app settings on server to add connection string. Example: "Server=[url];Database=CoolStuff;User Id=admin;Password=[password];"
* Run app again and test

Hint: if you ever need to kill an app that's using your port, use `fuser -k 5000/tcp`

Show updated diagram with RDS

## Setup Second Webserver

* Name subnet 2
* Create new EC2 instance as before, except *choose subnet-2*
* Copy code
* Change connection string
* Assign to previous security group (launch-wizard-x)
* Run the app
* Visit in browser


## Setup Loadbalancer

* Go to EC2 > Load Balancers
* Create Load Balancer
* Application Load Balancer
* Specify the two AZs/Subnets from earlier
* Step 2 (accept defaults)
* Step 3 create new security group and name it (do not start the name with "sg-")
* When the targets are added, make sure they are added with port 5000
* This will take several minutes to create and register targets
* Test by killing one of the servers

