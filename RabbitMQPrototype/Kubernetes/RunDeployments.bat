kubectl apply -f apigateway-deployment.yml
kubectl apply -f accountservice-deployment.yml
kubectl apply -f authservice-deployment.yml
kubectl apply -f ingress.yml
kubectl apply -f rabbitmq-deployment.yml

pause