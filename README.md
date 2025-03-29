## How to Execute

  ### Tools needed
    Docker Desktop is needed just to check or inspect the container.
	  https://docs.docker.com/desktop/setup/install/windows-install/


  ### Visual Studio
     Right click on the docker-compose project > Set as StartUp project. 
     Then click "Run"
 
  ### Rider 
     Left click on the docker-compose project and expand it.
     Then right click on the "docker-compose.yml" file and select "Run 'docker-compose.yml	'" 

 After following the steps above the container should be running with all the images already set up properly.
 All the communications between the WebApi, database and RabbitMq are already setup to make testing process easier.
 Now the project is ready to test.

## Auth Api 
- Address: https://localhost:5001/swagger/
- Description: This link provides access to swagger doc where it's possible to test the api and see how each method works.

## Keycloak 
- Address: http://localhost:7080/
- Description:  This link provides access to keycloak interface.

See [Keycloak Config](/.doc/keycloak-config.md)

## RabbitMq
- Address: http://localhost:15672/
- Description: This link provides access to the RabbitMq interface where it's possible to watch all the queues.

## Redis 
- Address: localhost:6379 or 127.0.0.1:6379
- Description: This link provides access to Redis database to check the cached keys.
- Tip: Use Redis Insight to see the cached keys via user interface. Just paste the ip when asked to do so.
