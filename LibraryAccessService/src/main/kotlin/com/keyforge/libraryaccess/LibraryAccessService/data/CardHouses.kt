package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "cardHouses")
data class CardHouses (
    @Id
    val id: Int? = null,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "cardId")
    val card: Card,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "houseId")
    val house: House
)