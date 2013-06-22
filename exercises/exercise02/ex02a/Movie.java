
/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005
*/

/**
 * Method for holding movie information
 */
class Movie {

   private String name;  // name of the movie
   private String time;   // the time the movie is displayed
   private String theater;  // the theater the movie is displayed in

   /**
    * Empty constructor
    */
   Movie()
    {
      name    = null;
      time    = null;
      theater = null;
    }

   /**
    * Constructor
    *
    * @param name, name of the movie
    * @param time, the time the movie is displayed
    * @param theater, the theater the movie is displayed in
    */
   Movie(String name, String time, String theater)
    {
      this.name = name;
      this.time  = time;
      this.theater = theater;
    }

   /**
    * Simple get method for getting the name of the movie
    *
    * @return String, name of the movie
    */
    public String getName()
    {
      return name;
   }

   /**
    * Simple get method for getting the time the movie is displayed
    *
    * @return String, the time the movie is displayed
    */
   public String getTime()
   {
      return time;
   }

   /**
    * Simple get method for getting the theater the movie is displayed in
    *
    * @return String, the theater the movie is displayed in
    */
   public String getTheater()
   {
      return theater;
   }

};