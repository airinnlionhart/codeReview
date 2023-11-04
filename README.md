# codeReview
This is a code review challenge for an API to return a list of candidates that have all the qualification answer  correctly.

I have no database connected, so this example is using a http post to check and sole purpose is for logic and to progress my skills in dotnet.
My Code is currently under the classes branch

For Monitor Performance and Health 

<Health>
Success Rate - SLI Http Post 200's Since this is a customer facing route I would like to maintain 99% success rate
Failures - SLI Http Post 400's Missing required fields if I had logs check to see what what is missing, 500 server errors or availability issues since it not reaching out to other api or servers

<Perfomance> K6 LoadTest
 data_received..................: 1.2 MB 39 kB/s
     data_sent......................: 1.6 MB 54 kB/s
     http_req_blocked...............: avg=1.3ms    min=0s       med=2µs     max=479.6ms  p(90)=5µs      p(95)=6µs     
     http_req_connecting............: avg=1.08µs   min=0s       med=0s      max=704µs    p(90)=0s       p(95)=0s      
     http_req_duration..............: avg=102.07ms min=759µs    med=81.13ms max=773.79ms p(90)=209.76ms p(95)=259.81ms
       { expected_response:true }...: avg=102.07ms min=759µs    med=81.13ms max=773.79ms p(90)=209.76ms p(95)=259.81ms
     http_req_failed................: 0.00%  ✓ 0         ✗ 2902
     http_req_receiving.............: avg=273.41µs min=10µs     med=37µs    max=25.94ms  p(90)=90.9µs   p(95)=149.94µs
     http_req_sending...............: avg=16.92µs  min=3µs      med=14µs    max=547µs    p(90)=25µs     p(95)=32µs    
     http_req_tls_handshaking.......: avg=1.3ms    min=0s       med=0s      max=479.08ms p(90)=0s       p(95)=0s      
     http_req_waiting...............: avg=101.78ms min=722µs    med=80.99ms max=773.72ms p(90)=209.48ms p(95)=259.7ms 
     http_reqs......................: 2902   96.441059/s
     iteration_duration.............: avg=103.58ms min=874.87µs med=81.38ms max=842.47ms p(90)=211.39ms p(95)=261.52ms
     iterations.....................: 2902   96.441059/s
     vus............................: 10     min=10      max=10
     vus_max........................: 10     min=10      max=10

running (0m30.1s), 00/10 VUs, 2902 complete and 0 interrupted iterations

Since this route would expect about this much traffic at peaktimes I would like to maintain a http_req_duration < 500ms  


SLOs
Succesrate of 99%
Latency under 500ms

