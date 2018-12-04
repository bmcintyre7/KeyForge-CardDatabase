package com.keyforge.libraryaccess.LibraryAccessService.services

import com.keyforge.libraryaccess.LibraryAccessService.repositories.CardRepository
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service

@Service
data class CardService(
    @Autowired
    val cardRepository: CardRepository
)