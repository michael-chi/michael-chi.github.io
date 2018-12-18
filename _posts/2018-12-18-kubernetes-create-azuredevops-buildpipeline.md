Create a Build pipeline
=======================

Next step I want to create a build pipeline to automatically build my application into Kubernetes applications. In order to do so, I need:

-   Understand how applications running on a k8s cluster
-   An Azure DevOps subscription
-   A Build pipeline that triggered by code commits

How applications running on a kubernets cluster
===============================================
[Kubernetes official website](https://kubernetes.io/docs/concepts/services-networking/service/)  actually provides very detailed information on how it manages applications. As that is not what I want to address here. 

In a high-level view, I need to create two deployments for my backend and frontend application. Then I need to create two services for each deployments.

A deployment is a minimal deplpyment unit, it can container one or more applications in it. These applications are considered a basic unit to deploy to k8s cluster, and should have same life cycle.

A service is an abstraction which defines a logical set of Pods and a policy by which to access them.

In order to have my frontend and backend running on kubernetes cluster, I need to create two deployment files, and two services file.

-   Create a Kubernetes namespace

-   Create a Kubernetes secret

-   Frontend deployment defination

```yaml
apiVersion: apps/v1
#   specify defination kind, which in this case is a Deployment
kind: Deployment
metadata:
#   This name will be use later for our service to pick corresponding deployment
  name: twotier-backend
#   To isolate our application with other application, we create a namespace as isolation layer. This namespace value can be pass in later in our pipeline - which is a better practice
#  namespace: myapp01
spec:
#   Here we specify how we want to "search" for our docker image
  selector:
    matchLabels:
      app: twotier-backend
      tier: backend
      track: stable
  replicas: 1
  template:
    metadata:
#      namespace: myapp01
      labels:
        app: twotier-backend
        tier: backend
        track: stable
    spec:
      containers:
#   Above selector will search for names that matches this name
        - name: twotier-backend
#   My docker image tag
          image: myazureacr001.azurecr.io/backend-app:latest
#   My container wants to export port 5106 for external commnunication
          ports:
            - name: http
              containerPort: 5106
      imagePullSecrets:
#   Here we pull our Container Registry credentail from kubernets secret we created in above step
        - name: myazureacrsecret001
```

-   Frontend Service defination file

```yaml
kind: Service
apiVersion: v1
metadata:
  name: app-backend
  namespace: jabilpreparation
spec:
  selector:
    app: twotier-backend
    tier: backend
  ports:
  - protocol: TCP
    port: 5106
    targetPort: http
  type: NodePort
```