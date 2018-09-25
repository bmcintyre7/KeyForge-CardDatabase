package com.keyforge.libraryaccess.LibraryAccessService

import org.springframework.boot.autoconfigure.EnableAutoConfiguration
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.autoconfigure.domain.EntityScan
import org.springframework.boot.runApplication
import org.springframework.data.jpa.repository.config.EnableJpaRepositories

@EnableAutoConfiguration
@EnableJpaRepositories
@EntityScan("com.keyforge.libraryaccess.LibraryAccessService.data")
@SpringBootApplication
class LibraryAccessServiceApplication

fun main(args: Array<String>) {
    runApplication<LibraryAccessServiceApplication>(*args)
}
