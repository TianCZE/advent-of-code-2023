import requests

url = 'https://adventofcode.com/2023/day/1/input'
cookies = {'Cookie': 'session=53616c7465645f5f4cb3d96080bc79e55ce663d6412b' +
           '67e6eeaf2891d83f0b47be1d4b6432c9b1fa77f4118adc7cf19cbdbf9a1c7f7f' +
           '4777910c43b0e48d531e'}

response = requests.get(url, cookies=cookies)

if response.status_code == 200:
    # Get the content from the response
    data = response.content

    # Specify the file path where you want to save the data
    file_path = f'day{1}_input.txt'

    # Open the file in binary write mode and write the data
    with open(file_path, 'wd') as file:
        file.write(data)

    print(f'Data saved to {file_path}')
else:
    print(f'Request failed with status code {response.status_code}')
    print(response.text)
