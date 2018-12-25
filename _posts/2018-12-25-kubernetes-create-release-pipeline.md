Create a release pipeline
=========================

In previous step we created a build pipeline which automatate build process whenever we commit our codes. Next step is to create a release pipeline to automatic deploy our applications to Kubernetes cluster.

-   Create a new Azure Release Pipeline

<img src="media/20181206-release-create-new-release-pipeline.jpg"/>

-   Start from an empty template

<img src="media/20181206-release-start-from-empty.jpg"/>

-   Once created, select our build source

<img src="media/20181225-release-select-build.jpg"/>

-   Once selected, add a new task

<img src="media/20181225-release-new-tasks.jpg"/>

-   Select agent pool, here we use Ubuntu

<img src="media/20181225-release-select-agent-pool.jpg"/>

-   Next, we want to deploy our backend application. We run kubectl apply command against our service defination file to deploy application.

    In order to have our pipelin task able to connect to our Kubernetes cluster, we need to create and specify a Service Connection here.

<img src="media/20181225-release-new-service-connection.jpg"/>

-   You will be prompt to input Kubernets connection information. In my lab environment I am using kubeconfig, if you configured Service Accoubt while creating kubernets cluster you can use it.

    -   Give this connection an user friendly name

    -   Kubeconfig file locates in your $HOME/.kube/config. Copy its content and paste into "KubeConfig" field


```shell
sudo cat $HOME/.kube/config
```

<img src="media/20181225-release-new-service-connection-1.jpg"/>

    -   Select Cluster Context after you paste KubeConfig

<img src="media/20181225-release-add-k8s-connection-1.jpg"/>

-   Check "Accept untrusted certificates", Verify connection then Click OK to close dialog.

<img src="media/20181225-release-new-service-connection-2.jpg"/>

-   Next, make sure we selected correct connection and then specify correct namespace that we created and will be using to isolate our applications.

<img src="media/20181225-release-select-service-connection-and-ns.jpg"/>

-   We use kubectl apply to deploy our applications. Check "Use Configuration files"

<img src="media/20181225-release-1.jpg"/>

-   Select our kubernetes defination file.

<img src="media/20181225-release-select-yaml-file.jpg"/>

-   Add below argument as we are using self-signed certificates

```shell
--insecure-skip-tls-verify=true
```

<img src="media/20181225-release-add-argument.jpg"/>

-   Clone this task, we want to deploy our backend application

<img src="media/20181206-release-clone-task.jpg"/>

-   Update cloned task to reference backend.yaml. And move backend task to first task. Then Save this Pipeline.

<img src="media/20181206-release-clone-task-2.jpg"/>

-   Start a new release, wait and verify the deployment.

<img src="media/20181206-release-verify-result.jpg"/>