

/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005
*/

/**
 * Concrete billboard display class
 */
class SimpleBillboard extends Billboard {

   // message to be displayed
   private String contents;

   /**
    * Constructor, creating a new Window and setting the fontsize
    */
   public SimpleBillboard()
   {
      // Creating billboard window must add 8 pixels in x direction and 40 in y direction
      // to get internal window of 400x28. (28 because its the double of font size 14)
      output = new Window("Simple Billboard",408,68);

      // Setting the font and size for the text in the window.
      output.setFont("Courier New", 14);
      
   }

   /**
    * Method responsible for putting new text in the window and scrolling it to make room for
    * the next text
    *
    * @param Movie The movie information which is to be displayed in the window
    */
   public void showNextMovie(Movie movie)
   {
      // Create text to be added to the display
      contents = movie.getName() + " - " + movie.getTheater() + ", " + movie.getTime() + " ** ";

      // calculate where text should be placed initially
      int y = ( (output.getHeight() - output.getFontMetrics(output.getFont()).getHeight() ) / 2 ) + output.getFont().getSize();
      int x = output.getWidth();

      // Draw the content on screen just outside the right border of the window
      output.drawString(contents, x, y);

      // Find how long the content is in pixels
      int textLength = output.fontWidth(contents);

      // Scroll the content into the window
      output.moveText(textLength);
   }
};