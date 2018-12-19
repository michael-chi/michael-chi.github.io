---
layout: post
title: "Build an Azure Container Registry"
categories:
  - k8s
  - kubernetes
  - microservice
---

Build Azure Container Registry
==============================
Now that I have my sample docker container application running and have them talk to each other. Next, I'd like to setup environment to do CI/CD. Here I will be using Azure DevOps as my CI/CD tool, and use Azure Container Registry to simulate using a private container registry to store my docker images.

It's very simple to setup an Azure Container Registry

- Go to Azure Portal, create a new Azure Container Registry as you create other Azure services

  -   Note that if you disable "Admin User", you would need to create a service principal in Azure AD so that later you use that service principal credential to login to Azure Container Registry.

        If you enable "Admin User" here, you can use Azure created user Id and password to login to ACR later. This is NOT recommand in production environment, but for our development we choose to enable it to simplify necessary steps

<img src="media/20181218-create-acr.jpg">

-   Once complete creation, goto Azure Container Registry console, Access Keys. You can find Username and Password here, which we can use later to login to Azure Container Registry.
-   You can also find Login Server URL here, note down the url, we will need it later.

<img src="media/20181218-acr-console.jpg"/>

Push docker images to Azure Containr Registry
=============================================

- Now we have our ACR up and running, I want to push my newly created docker images to my Azure Container Registry. Execute below command to login to my ACR
  - User Name, Password and Login Server URI are retrieved from ACR console shown above
  - 

```shell
docker login -p <PWD> -u <USER> myacrdemo001.azureacr.io
```

- Then we push our images to ACR

```shell
docker tag kalschi/frontend <ACRNAME>.azurecr.io/<IMAGE NAME>

docker push <ACRNAME>.azurecr.io/<IMAGE NAME>
```

<img src="media/20181220-docker-push.jpg"/>

- Push both frontend and backend to ACR.

<img src="media/20181218-acr-images.jpg" />

Azure DevOps
============

Azure Pipelines allows you to continously build, test, deploy from source controls of your choice to any platform and cloud of your choice. In addition to built-in host, tasks. It alos allows you to create your own agent host in your environment to fulfill special requirements.

In this lab I am using Azure Pipelines to build my applications into docker images, then deploy them to my kubernetes cluster.

- Create Azure DevOps

To start, go to https://devops.azure.com and start free. Free tier come with 5 users with full features.

- Create Project

Once signed in, click "Create Project" to start a new project

<img src="media/20181218-devops-create-project.jpg"/>

- Once created, notedown the command and url to push your code, we will need them later

<img src="media/20181220-devops-project-init.jpg"/>

- Next we want to create an access token for us to access our Repo from local machine. Go to Security tab
  
<img src="media/20181220-create-token-1.jpg" />

- Create a new Token

<img src="media/20181220-create-token-2.jpg" />

- Give this token full access, hit "Create". Note that once you hit create, Token will be shown on the page and it will not be shown again once you go next step. SO NOTE IT DOWN here.

<img src="media/20181220-create-token-3.jpg" />

- Now go to your development machine, switch to your source code folder. We need to push our codes.

```shell
git init
git add .
git commit -m "init"

```

<img src="media/20181220-acr-commit-codes.jpg"/>

- Then use those commands we noted to push our codes

```
git remote add origin https://<YOUR USER>@dev.azure.com/<YOUR ORG>/<YOUR PROJECT>/_git/<YOUR PROJECT>

git push -u origin --all
```

<img src="media/20181220-git-push.jpg" />

- When promoted to enter password, input the access token you created

<img src="media/20181220-my-repo.jpg" />