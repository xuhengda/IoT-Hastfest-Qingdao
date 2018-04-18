package com.mycompany.app;
import com.microsoft.azure.sdk.iot.device.*;
import com.google.gson.Gson;

import java.io.*;
import java.net.URISyntaxException;
import java.util.Date;
import java.util.Random;
import java.util.concurrent.Executors;
import java.util.concurrent.ExecutorService;
/**
 * Hello world!
 *
 */
public class App
{
	private static String connString = "HostName=myTeam.azure-devices.net;DeviceId=myTeam.azure-devices.net;SharedAccessKey=...";
	private static IotHubClientProtocol protocol = IotHubClientProtocol.MQTT;
	private static String deviceId = "myTeam.azure-devices.net";
	private static DeviceClient client;

	public static void main( String[] args ) throws IOException, URISyntaxException {
		  client = new DeviceClient(connString, protocol);
		  client.open();

		  MessageSender sender = new MessageSender();

		  ExecutorService executor = Executors.newFixedThreadPool(1);
		  executor.execute(sender);

		  System.out.println("Press ENTER to exit.");
		  System.in.read();
		  executor.shutdownNow();
		  client.closeNow();
		}

    private static class TelemetryDataPoint {
//    	  public String deviceId;
//    	  public double temperature;
//    	  public double humidity;
    	  public String ticketId;
    	  public String entryTime;

    	  public String serialize() {
    	    Gson gson = new Gson();
    	    return gson.toJson(this);
    	  }
    	}

    private static class EventCallback implements IotHubEventCallback {
    	  public void execute(IotHubStatusCode status, Object context) {
    	    System.out.println("IoT Hub responded to message with status: " + status.name());

    	    if (context != null) {
    	      synchronized (context) {
    	        context.notify();
    	      }
    	    }
    	  }
    	}

    private static class MessageSender implements Runnable {
    	  public void run()  {
    	    try {
    	      double minTemperature = 20;
    	      double minHumidity = 60;
    	      Random rand = new Random();

    	      while (true) {
    	        double currentTemperature = minTemperature + rand.nextDouble() * 15;
    	        double currentHumidity = minHumidity + rand.nextDouble() * 20;
    	        TelemetryDataPoint telemetryDataPoint = new TelemetryDataPoint();
//    	        telemetryDataPoint.temperature = currentTemperature;
//    	        telemetryDataPoint.humidity = currentHumidity;
    	        telemetryDataPoint.ticketId = java.util.UUID.randomUUID().toString();
    	        Date d = new Date();
    	        telemetryDataPoint.entryTime = String.valueOf(d.toLocaleString());

    	        String msgStr = telemetryDataPoint.serialize();
    	        Message msg = new Message(msgStr);
//    	        msg.setProperty("temperatureAlert", (currentTemperature > 30) ? "true" : "false");
    	        msg.setMessageId(java.util.UUID.randomUUID().toString());
    	        System.out.println("Sending: " + msgStr);

    	        Object lockobj = new Object();
    	        EventCallback callback = new EventCallback();
    	        client.sendEventAsync(msg, callback, lockobj);

    	        synchronized (lockobj) {
    	          lockobj.wait();
    	        }
    	        Thread.sleep(1000);
    	      }
    	    } catch (InterruptedException e) {
    	      System.out.println("Finished.");
    	    }
    	  }
    	}
}
