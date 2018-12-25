---
layout: post
title: "My Kubernetes learning"
categories:
  - k8s
  - kubernetes
  - microservice
---

My Kubernetes learning
======================
So recently I am pulled into a project that customer wants to use microservice to build their new platform. I never experienced microservice before, but it's always good to learn something new. Most attractively, we can ~~test~~ work on a production microservice application !

So here I will be bloging what I've done to learn microservice (kubernetes) during my preparation sprint, the scenario I try to create, problems I faced and solution I found (may not be a perfect one, you know).

<h2>Requirements</h2>

So we know there are some requirements.
- Has to be able to isolated develop, deploy, manage and operate for different applications on to this platform.
- Has to integrated with CI/CD pipeline
- Auto-scale
- High Availability

There are some other business requirements but we are not going into those here. Those are very common requirements while talking about a production application. We've got a lot to do just to fulfill above requirements.

<h2>PoC</h2>
So I want to start a proof of concept application that has below features

- A Web UI frontend that retrieve something from a backend REST API
- A backend REST API that provides an endpoint for my frontend application to retrieve some random messages
- An infrastructure that allows frontend to talk to backend
- Use Azure DevOps as Source control and CI/CD Pipeline
- Configuration, credential or secrets should be stored separately without deverloper access. (we developers, always mess up something ! That's how we feel alive ! :smile: )
- Kubernetes (because it's cool !)

I haven't think of how to scale or HA yet, but these should be enough for me to busy for a while.

<h2>Tasks</h2>
Now I have a target, let's break them into smaller tasks. I need...

- Setup a k8s cluster
    - I will use Azure virtual machines to setup kubernetes cluster
- Deploy a sample hello-world application to the cluster to ensure it works
- Create a containerized backend application that provides REST API to return some random messages
- Create a containerized frontend web application that retrieves some messages from backend application
- Create a build pipeline to build docker images
- Create a release pipeline to deploy applications to my cluster

<h2>What's next</h2>
I will blog task by task to accomplish above goal. There has been a lot documents talking about setup Kubernetes cluster. So I will not repeat them. For me,I just followed <a href="https://kubernetes.io/docs/getting-started-guides/ubuntu/installation/">official documents</a> to setup an two node (master and slave) cluster.

- <a href="./2018-12-06-kubernetes-create-sample-app.md">Create a sample 2-tier hello-world application</a>
- <a href="./2018-12-18-kubernetes-create-devops-and-acr.md">Use Azure Container Registry</a>
- <a href="./2018-12-18-kubernetes-manual-deployment.md">Manual Deployment</a>
- <a href="./2018-12-25-kubernetes-devops-deployment.md">Create a Build Pipeline</a>
- <a href="./2018-12-25-kubernetes-create-release-pipeline.md">Create a Release Pipeline</a>
- Running on Kubernetes
- Configure Ingress