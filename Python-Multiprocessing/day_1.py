def filter_out_letters(string: str) -> str:
    return ''.join(filter(lambda s: str.isdigit(s), string))


if __name__ == "__main__":
    with open('day_1_input.txt', 'r') as file:
        content = file.read()
        print(filter_out_letters(content))
