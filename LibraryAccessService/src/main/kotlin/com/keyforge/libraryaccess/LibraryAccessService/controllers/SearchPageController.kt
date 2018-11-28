package com.keyforge.libraryaccess.LibraryAccessService.controllers

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.data.House
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardListBody
import jdk.nashorn.internal.objects.NativeJSON.stringify
import com.google.gson.Gson
import com.keyforge.libraryaccess.LibraryAccessService.repositories.*
import com.keyforge.libraryaccess.LibraryAccessService.responses.SearchInfoBody
import org.springframework.web.bind.annotation.*

@RestController
class SearchPageController (
        private val houseRepository: HouseRepository,
        private val typeRepository: TypeRepository,
        private val keywordRepository: KeywordRepository,
        private val traitRepository: TraitRepository
) {
    @RequestMapping(value ="/searchInfo", method = [RequestMethod.GET])
    fun getSearchInfo() : SearchInfoBody {
        return SearchInfoBody(
                houses = houseRepository.findAll().map { house -> house.toSearchInfoString() }.toList(),
                traits = traitRepository.findAll().map { trait -> trait.toSearchInfoString() }.toList(),
                types = typeRepository.findAll().map { type -> type.toSearchInfoString() }.toList(),
                keywords = keywordRepository.findAll().map { keyword -> keyword.toSearchInfoString() }.toList()
        )
    }
}