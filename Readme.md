# Project Title

A Poke List API

## Overview

This is a Pokemon API that connects to the Pokemon API and caches the results inside Redis to deliver a full working solution of Http Client and Caching with Redis and docker using dotnet core 2.0.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

You're going to need Redis, I'm using an Docker image and you can get yourself one by running:

```
docker run -d --name myRedis -p 6379:6379 redis
```

### Installing

You can get your development env running by following these steps:

Get the code

```
git clone 
```

Restore dotnet packages

```
dotnet restore
```

Navigate to http://localhost:5000/api/pokemon to get a list of pokemons
Navigate to http://localhost:5000/api/pokemon/1 to get a single pokemon


## Built With

* [Redis](https://redis.io) - For distributed caching
* [DotNetCore](https://dotnet.github.io) - Web server
* [Docker](https://www.docker.com) - Ship web server and caching server as containers

## Authors

* **David Santos** - *Initial work* - [DavidSantos](https://github.com/davidsellen)

See also the list of [contributors](https://github.com/davidsellen/pokelist/contributors) who participated in this project.

## License

This project is licensed under the MIT License

## Acknowledgments

* This API cosumes its pokemons from https://pokeapi.co
* Used some ideas from [Renato Groffe](https://medium.com/@renato.groffe/net-core-2-0-nosql-exemplos-utilizando-mongodb-documentdb-e-redis-be5f5407ff13)
