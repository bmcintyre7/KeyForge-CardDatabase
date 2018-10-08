package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.data.House
import com.keyforge.libraryaccess.LibraryAccessService.repositories.KeywordRepository
import com.keyforge.libraryaccess.LibraryAccessService.repositories.HouseRepository
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import jdk.nashorn.internal.objects.NativeJSON.stringify
import com.google.gson.Gson
import org.springframework.web.bind.annotation.*

@RestController
class KeywordsController (
        private val keywordRepository: KeywordRepository
) {
    @RequestMapping(value ="/keywords/{id}", method = [RequestMethod.GET])
    fun getKeyword(@PathVariable("id") id: Int) : String {
        return keywordRepository.getOne(id).name
    }

    @RequestMapping(value ="/keywords", method = [RequestMethod.GET])
    fun getKeywords() : String {
        val gson = Gson()
        var keywords = keywordRepository.findAll()
        var responseData = mutableListOf<String>()
        for (keyword in keywords) {
            responseData.add(keyword.name)
        }
        return responseData.joinToString(", ")
    }
}