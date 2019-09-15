# What I run

 - install kubectl
 - install az cli
 
 ## Run

```
# create services
kubectl apply -f master/web.yml

# check status
kubectl get service forno-finpe-api

# give access to web ui
kubectl create clusterrolebinding kubernetes-dashboard --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

# access web ui
az aks browse --resource-group kubernets --name tiago-k8s  

```