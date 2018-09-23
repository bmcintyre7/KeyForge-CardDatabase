package com.keyforge.libraryaccess.LibraryAccessService

import org.springframework.boot.autoconfigure.EnableAutoConfiguration
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication

@EnableAutoConfiguration
@SpringBootApplication
class LibraryAccessServiceApplication

fun main(args: Array<String>) {
    runApplication<LibraryAccessServiceApplication>(*args)
}
