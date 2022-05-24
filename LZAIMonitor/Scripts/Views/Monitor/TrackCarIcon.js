const GPSIcon = function () {
    return {
        //CarType:車子類型,Heading:方向, IO1:開機訊號: 1 發動 2 斷電續傳 0 熄火, Speed:速度
        CarIcon: function (CarType, Heading, IO1, Speed) {
            var Url = "../images/Car/";
            if (IO1 == 0) //熄火
            {
                Url += "G/";
            } else {
                if (Speed == 0)
                    Url += "Y/";
                else
                    Url += "B/";
            }

            switch (CarType) {
                case "7":
                    Url += "Pig/";
                    break;
                case "6":
                    Url += "R/";
                    break;
                case "5":
                    Url += "D/";
                    break;
                case "4":
                    Url += "E/";
                    break;
                case "3":
                    Url += "C/";
                    break;
                case "2":
                    Url += "B/";
                    break;
                case "1":
                    Url += "A/";
                    break;
                default:
                    Url += "Not/";
                    break;
            }
            if (Heading >= 22.5 && Heading < 67.5) {
                Url += "car_rt.png";
            } else if (Heading >= 67.5 && Heading < 112.5) {
                Url += "car_r.png";
            } else if (Heading >= 112.5 && Heading < 157.5) {
                Url += "car_rb.png";
            } else if (Heading >= 157.5 && Heading < 202.5) {
                Url += "car_b.png";
            } else if (Heading >= 202.5 && Heading < 247.5) {
                Url += "car_lb.png";
            } else if (Heading >= 247.5 && Heading < 292.5) {
                Url += "car_l.png";
            } else if (Heading >= 292.5 && Heading < 337.5) {
                Url += "car_lt.png";
            } else {
                Url += "car_t.png";
            }
            return Url;
        }
        , GetCarImg: function (GpsHeading, IconType) {
            var image = '';

            if (GpsHeading >= 337.5 || GpsHeading < 22.5) {
                image = 'images/car/' + IconType + 'car_t.gif';
            }
            if (GpsHeading >= 22.5 && GpsHeading < 67.5) {
                image = 'images/car/' + IconType + 'car_rt.gif';
            }
            if (GpsHeading >= 67.5 && GpsHeading < 112.5) {
                image = 'images/car/' + IconType + 'car_r.gif';
            }
            if (GpsHeading >= 112.5 && GpsHeading < 157.5) {
                image = 'images/car/' + IconType + 'car_rb.gif';
            }
            if (GpsHeading >= 157.5 && GpsHeading < 202.5) {
                image = 'images/car/' + IconType + 'car_b.gif';
            }
            if (GpsHeading >= 202.5 && GpsHeading < 247.5) {
                image = 'images/car/' + IconType + 'car_lb.gif';
            }
            if (GpsHeading >= 247.5 && GpsHeading < 292.5) {
                image = 'images/car/' + IconType + 'car_l.gif';
            }
            if (GpsHeading >= 292.5 && GpsHeading < 337.5) {
                image = 'images/car/' + IconType + 'car_lt.gif';
            }
            return '../' + image;
        }
        , GetArrowImg: function (GpsHeading) {
            var image = '';
            if (GpsHeading >= 337.5 || GpsHeading < 22.5) {
                image = 'images/GPS_detail/gps_direction_1.gif';
            }
            if (GpsHeading >= 22.5 && GpsHeading < 67.5) {
                image = 'images/GPS_detail/gps_direction_8.gif';
            }
            if (GpsHeading >= 67.5 && GpsHeading < 112.5) {
                image = 'images/GPS_detail/gps_direction_2.gif';
            }
            if (GpsHeading >= 112.5 && GpsHeading < 157.5) {
                image = 'images/GPS_detail/gps_direction_5.gif';
            }
            if (GpsHeading >= 157.5 && GpsHeading < 202.5) {
                image = 'images/GPS_detail/gps_direction_3.gif';
            }
            if (GpsHeading >= 202.5 && GpsHeading < 247.5) {
                image = 'images/GPS_detail/gps_direction_6.gif';
            }
            if (GpsHeading >= 247.5 && GpsHeading < 292.5) {
                image = 'images/GPS_detail/gps_direction_4.gif';
            }
            if (GpsHeading >= 292.5 && GpsHeading < 337.5) {
                image = 'images/GPS_detail/gps_direction_7.gif';
            }
            return '../' + image;
        }
    }
}();

export { GPSIcon }