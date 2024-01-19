use std::fs::File;
use std::io::{self, BufRead, ErrorKind, Read};

fn read_input() -> io::Result<(String)> {
    // Open the file in read-only mode
    let file_path = "day1_input";
    let mut file = File::open(file_path)?;

    // Read the contents of the file into a String
    let mut contents = String::new();
    file.read_to_string(&mut contents)?;

    Ok(contents)
}


fn extract_calibration_value(line: &str) -> u32 {
    let digits = line.chars().filter(|c| c.is_digit(10));

    let value: String = digits.collect();
    value.parse().unwrap_or(0)
}


fn main() {
    match read_input() {
        Ok(contents) => {
            println!("{}", contents);
        }
        Err(e) => {eprintln!("Error opening input: {}", e)}
    }
}

