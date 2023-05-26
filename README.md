Guide to Create and Run Docker Images



Frontend 

	ReactApp – Go to react-geo-graphic-ui folder and run command on cmd 
	---------------------------------------------------------------------
	docker build . -t geolocationui
	
	docker run -p 3000:3000 -d geolocationui



Microservice Services


	1. LocationMicroservice – Go to LocationMicroservice folder and run command on cmd
	----------------------------------------------------------------------------------
	
	docker build -f "Dockerfile" --force-rm -t locationmicroservice  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=LocationMicroservice" "../"
	
	docker run -p 5001:80 -d locationmicroservice


	2. GeoLocationDemo – Go to GeoLocationDemo folder and run command on cmd
	-------------------------------------------------------------------------------
	
	docker build -f "Dockerfile" --force-rm -t geolocationdemo  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=GeoLocationDemo" "../"
	
	docker run -p 5002:80 -d geolocationdemo




