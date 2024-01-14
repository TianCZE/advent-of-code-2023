use reqwest;
use std::io::{self, BufRead, ErrorKind};
use reqwest::{Error, Response};
use reqwest::header::HeaderMap;

fn fetch_input() -> Result<String, Error> {
    let url = "https://adventofcode.com/2023/day/1/input";
    let cookie_value = "session=53616c7465645f5f4033be0dacaa452ef015723a98ef249dadea2d44a4325fd1a9d7ddeb7b477973c8a7a71bc875cadb46ddddc7c1864b3338a0fb005c8b6c1b";

    let client = reqwest::blocking::Client::new();

    let cookie_header = reqwest::header::HeaderValue::from_str(cookie_value)
        .expect("Failed to create cookie header");

    // Create a HeaderMap and insert the cookie
    let mut headers: HeaderMap = HeaderMap::new();
    headers.insert(reqwest::header::COOKIE, cookie_header);

    // Send the GET request with the cookie and handle the response
    let response = client.get(url).headers(headers).send()?;


    // Read the response body as a string
    let body = response.text()?;
    Ok(body)
    Err(Interrupted)
}


fn extract_calibration_value(line: &str) -> u32 {
    let digits = line.chars().filter(|c| c.is_digit(10));

    let value: String = digits.collect();
    value.parse().unwrap_or(0)
}


fn main() {
    match fetch_input(){
        Ok(body) => {
            let total_sum: u32 = body
                .lock()
                .lines()
                .map(|line| line.expect("Failed to read line"))
                .map(|input_line| extract_calibration_value(&input_line))
                .sum();

            println!("Sum of all calibration values: {}", total_sum);
        }
        Err(err) => eprint!("Error: {}", err),
    }
}

