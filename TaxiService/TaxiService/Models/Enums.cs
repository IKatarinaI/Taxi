using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiService.Models
{
    public enum GRADE
    {
        UNDEFINED,      // 0
        ONE_STAR,       // 1
        TWO_STARS,      // 2    
        THREE_STARS,    // 3
        FOUR_STARS,     // 4 
        FIVE_STARS      // 5
    }

    public enum GENDER
    {
        FEMALE,
        MALE
    }

    public enum STATUS
    {
        CREATED,    //Musterija je kreirala voznju, bez vozaca - nema komentara
        FORMED,     //Dispecer je formirao voznju, bez vozaca - nema komentara
        PROCESSED,  //Dispecer je dodelio vozaca voznji - nema komentara
        ACCEPTED,   //Vozac je preuzeo voznju - nema komentara
        CANCELED,   //Musterija je kreirala voznju, pa otkazala - Musterija obavezno pravi komentar
        FAILED,     //Vozac zadaje da je voznja neuspesna - Vozac obavezno kreira komentar
        SUCCEEDED   //Vozac zadaje da je voznja uspesna - Musterija opciono kreira komentar
    }

    public enum CARTYPES
    {
        PASSENGER,
        VAN
    }

    public enum ROLE
    {
        CUSTOMER,
        ADMIN,
        DRIVER
    }
}