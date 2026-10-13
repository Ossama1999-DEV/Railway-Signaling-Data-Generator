cmake_minimum_required(VERSION 3.16)

project(RailwaySignalConsumer LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(tinyxml2 REQUIRED)

add_executable(RailwaySignalConsumer main.cpp)

target_link_libraries(
    RailwaySignalConsumer
    PRIVATE
    tinyxml2::tinyxml2
)