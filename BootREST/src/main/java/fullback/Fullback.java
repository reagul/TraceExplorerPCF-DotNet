package fullback;

import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;
import org.springframework.web.bind.annotation.RequestMapping;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import java.io.*;
import java.net.*;
import java.util.*;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cloud.sleuth.SpanAccessor;
import org.springframework.context.annotation.Bean;
import org.springframework.cloud.sleuth.Span;

@RestController
public class Fullback {
    private static Logger logger = LoggerFactory.getLogger(Fullback.class);

    @Autowired
    private SpanAccessor accessor;
   

    @RequestMapping("/")
    public String index() throws Exception {
        Span currentSpan = this.accessor.getCurrentSpan();
        List<Long> parents = currentSpan.getParents();
        List<String> parentsHex = new ArrayList<String>();

        for (int i = 0; i < parents.size(); i++) {
            String hex = String.format("%016x", parents.get(i));
            parentsHex.add(hex);
        }
       // String traceID = String.format("%016x", currentSpan.getTraceId());
       // String spanID = String.format("%016x", currentSpan.getSpanId());

        logger.info("Receiving ball from Quarterback");
        
        //setting up a random wait for up to 400 ms. 
        long range = 400;
        Random r = new Random();
        long number = (long)(r.nextDouble()*range);
        
        Long wait = (long) number;
        
        //Emits log that should show up in Trace explorer (Sleuth style formatting).  
           logger.info(" running with the ball for the next "+wait.toString() + " ms");

           // Here is the actual wait from the above.  This simulates processing time for a call.
        Thread.sleep(wait);
      
        logger.info("Done running with the ball.  Handing back control to QB");


        return "Got the ball and ran with it!";
    }
    
    
    
    @RequestMapping("/longrun")
    public String Index() throws Exception {
        Span currentSpan = this.accessor.getCurrentSpan();
        List<Long> parents = currentSpan.getParents();
        List<String> parentsHex = new ArrayList<String>();

        for (int i = 0; i < parents.size(); i++) {
            String hex = String.format("%016x", parents.get(i));
            parentsHex.add(hex);
        }
        String traceID = String.format("%016x", currentSpan.getTraceId());
        String spanID = String.format("%016x", currentSpan.getSpanId());

        logger.info("Receiving ball from Quarterback on /longrun end point");
        
        
        //Here we do the same random wait up to 400 then add 400 onto the value to make this wait between 401 and 800 ms
        long range = 400;
        Random r = new Random();
        long number = (long)(r.nextDouble()*range);
        number =number + 400;
        Long wait = (long) number;
        
        //Emits log that should show up in Trace explorer (Sleuth style formatting).  
        logger.info("Running with the ball for the next "+wait.toString() + " ms");

        //Where we actually do the wait
      Thread.sleep(wait);
      
      logger.info("Done running with the ball.");


    return "Got the ball and ran with it!";
    }

}
