package com.keyforge.libraryaccess.LibraryAccessService.services

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import com.keyforge.libraryaccess.LibraryAccessService.repositories.CardRepository
import com.keyforge.libraryaccess.LibraryAccessService.responses.CardBody
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service

@Service
data class CardService(
    @Autowired
    val cardRepository: CardRepository
)