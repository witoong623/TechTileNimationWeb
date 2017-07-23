import argparse
import os
import subprocess

from os import path

def parse_args():
    parser = argparse.ArgumentParser(description='Script for building TechtileNimation')
    parser.add_argument('--docker', '-d')

def main():
    print('start building.')
    subprocess.run('dotnet publish -f netcoreapp1.1 -c Release')
    print('finished building.\nstart build dockerfile')
    subprocess.run('docker build -t techtilenimation bin/Release/netcoreapp1.1/publish/')
    print('finished building docker image')


if __name__ == '__main__':
    main()