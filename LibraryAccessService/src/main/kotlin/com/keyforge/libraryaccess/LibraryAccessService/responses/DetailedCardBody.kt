package com.keyforge.libraryaccess.LibraryAccessService.responses

import com.keyforge.libraryaccess.LibraryAccessService.data.Card

data class DetailedCardBody (
    val name: String = "",
    val type: String = "",
    val text: String = "",
    val aember: String? = null,
    val armor: String? = null,
    val power: String? = null,
    val rarity: String = "",
    val artist: String = "",
    val imageNames: MutableList<String>,
    val expansions: MutableList<String>,
    val houses: MutableList<String>,
    val keywords: MutableList<String>,
    val traits: MutableList<String>
) {
    object ModelMapper {
        fun from(from: Card) {
            var expansions = mutableListOf<String>()
            var imageNames = mutableListOf<String>()
            var houses = mutableListOf<String>()
            var keywords = mutableListOf<String>()
            var traits = mutableListOf<String>()

            for (expansion in from.expansions) {
                expansions.add(expansion.expansion.name + " #" + expansion.number)
                imageNames.add(expansion.expansion.name.toLowerCase() + "-" + expansion.number)
            }

            for (house in from.houses) {
                houses.add(house.name)
            }

            for (keyword in from.keywords) {
                keywords.add(keyword.keyword.name)
            }

            for (trait in from.traits) {
                traits.add(trait.name)
            }

            DetailedCardBody(from.name, from.type.name, from.text, from.aember, from.armor, from.power, from.rarity.name, from.artist, imageNames, expansions, houses, keywords, traits)
        }
    }
}