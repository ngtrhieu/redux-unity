#!/usr/bin/env bash

# Create aws profile

if [ -z $AWS_ACCESS_KEY ] || [ -z $AWS_ACCESS_SECRET ]; then
  echo -e "\033[0;31m:: Failed to generate AWS profile\033[0m"
  echo -e "\033[0;31mEither AWS_ACCESS_KEY or AWS_ACCESS_SECRET is not set.\033[0m"
  exit 1
fi

echo :: Generating aws profile...

mkdir -p ~/.aws

cat >~/.aws/credentials <<EOL
[default]
aws_access_key_id = ${AWS_ACCESS_KEY}
aws_secret_access_key = ${AWS_ACCESS_SECRET}
EOL

echo :: aws profile generated at ~/.aws/credentials
