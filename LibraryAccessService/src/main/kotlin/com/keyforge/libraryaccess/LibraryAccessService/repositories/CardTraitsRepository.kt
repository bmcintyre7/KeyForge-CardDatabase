package com.keyforge.libraryaccess.LibraryAccessService.repositories

import com.keyforge.libraryaccess.LibraryAccessService.data.CardExpansions
import com.keyforge.libraryaccess.LibraryAccessService.data.CardHouses
import com.keyforge.libraryaccess.LibraryAccessService.data.CardTraits
import com.keyforge.libraryaccess.LibraryAccessService.data.Type
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository

@Repository
interface CardTraitsRepository : JpaRepository<CardTraits, Int> {
    fun findByCardId(id: Int) : CardTraits
    fun findByTraitId(id: Int) : CardTraits
}