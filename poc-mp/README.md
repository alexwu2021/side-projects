# README #

This is a POC project aimed at exploring for an object way to answer aggregated KPI queries
  

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

## where to set the connection string for db access
/poc-mp/poc-mp/src/main/resources/application.yml
/poc-mp/poc-mp/META-INF/${profile}/persistence.xml
/poc-mp/poc-mp/src/main/resources/${profile}/hibernate.cfg.xml

spring:
datasource:
username: xyz-feed
password: ## left out
driver-class-name: com.mysql.cj.jdbc.Driver
url: jdbc:mysql://1.2.3.4:3306/dw


### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###
* Please contact 


## design ideas
1) using hash map to store sparse data
2) store only coded data, that is, just integers
3) form intermediate data structure to answer aggregated queries, and cache them whenever possible

## postman testing 

localhost:9002/ims/stat/factFileCount
localhost:9002/ims/stat/openEmail/all with dates specified like 20211101,20211115
