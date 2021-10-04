# takehome-project

## About

Screen record: 
![alt text](https://github.com/sheldonyss/takehome-project/blob/master/2021-09-14_07-52-40.gif?raw=true "screen record")

APIs used:
https://stripe.com/docs/api/payment_intents/create
https://stripe.com/docs/stripe-js

To be improved:
1.  The currency is fixed to SGD. To support other currencies, an utility class is desired to handle the currency display from its smallest unit.
2.  For simplicity, this project does not include customer membership management, order management, and other commonly seen features provided in e-commerce software. This means you can pay for the same item many times.
3.  Allow customer to cancel order and automatically trigger refund.
4.  More payment methods
  
## Certificate Issues
This project uses a self-signed certificate and it is not trusted by the Chrome browser by default. Therefore, you may not be able to pay in your brwoser.
In that case, you must **manually trust the self-signed certificate** first before you can try the payment flow. See https://www.pico.net/kb/how-do-you-get-chrome-to-accept-a-self-signed-certificate/ for the details.

## Run
```
git clone https://github.com/sheldonyss/takehome-project.git
cd takehome-project
cd StripeBookStore
docker build -t takehome -f Dockerfile ..
docker run -p 9999:80 takehome
```
