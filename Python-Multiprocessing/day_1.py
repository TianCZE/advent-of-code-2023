def filter_out_letters(content: str):
    result = ""
    for char in content:
        if char.isalpha:
            result += char
    return content


if __name__ == "__main__":
    with open('day_1_input.txt', 'r') as input:
        content = input.read()
        print(filter_out_letters(content))
