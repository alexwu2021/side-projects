package com.xyz.ims;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.data.mongo.MongoDataAutoConfiguration;
import org.springframework.boot.autoconfigure.mongo.MongoAutoConfiguration;
import org.springframework.boot.web.servlet.support.SpringBootServletInitializer;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.PropertySource;

import javax.security.sasl.SaslException;
import java.io.FileNotFoundException;
import java.sql.SQLException;

@PropertySource("classpath:dev/application.yml")
@Configuration
@SpringBootApplication(exclude = {
	MongoAutoConfiguration.class, MongoDataAutoConfiguration.class
})
public class ImsApplication extends SpringBootServletInitializer {

	public ImsApplication(){}

	public static void main(String[] args) throws SQLException, ClassNotFoundException, SaslException, FileNotFoundException {
		Class[] configClasses = new Class[]{
				ImsApplication.class,
		};
		SpringApplication.run(configClasses, args);

//		// triggers the health monitoring mechanism
		new HealthMonitoringDriver();

//		// for processing dw feed events
		new DwFeedEventProcessor();

	}

}
