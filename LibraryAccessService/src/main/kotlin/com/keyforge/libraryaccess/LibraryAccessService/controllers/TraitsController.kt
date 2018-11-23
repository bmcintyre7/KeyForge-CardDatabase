package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.repositories.TraitRepository
import org.springframework.web.bind.annotation.*

@RestController
class TraitsController (
        private val traitRepository: TraitRepository
) {
    @RequestMapping(value ="/traits/{id}", method = [RequestMethod.GET])
    fun getType(@PathVariable("id") id: Int) : String {
        return traitRepository.getOne(id).name
    }

    @RequestMapping(value ="/traits", method = [RequestMethod.GET])
    fun getTypes() : String {
        var traits = traitRepository.findAll()
        var responseData = mutableListOf<String>()
        for (trait in traits) {
            responseData.add(trait.name)
        }
        return responseData.joinToString(", ")
    }
}