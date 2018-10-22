package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.repositories.TypeRepository
import org.springframework.web.bind.annotation.*

@RestController
class TypesController (
        private val typeRepository: TypeRepository
) {
    @RequestMapping(value ="/types/{id}", method = [RequestMethod.GET])
    fun getType(@PathVariable("id") id: Int) : String {
        return typeRepository.getOne(id).name
    }

    @RequestMapping(value ="/types", method = [RequestMethod.GET])
    fun getTypes() : String {
        var types = typeRepository.findAll()
        var responseData = mutableListOf<String>()
        for (type in types) {
            responseData.add(type.name)
        }
        return responseData.joinToString(", ")
    }
}