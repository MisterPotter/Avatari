<?php
// src/Appbundle/Entity/Fitbit.php
namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
* @ORM\Entity
* @ORM\Table(name="fitbit")
*/
class Fitbit
{
    /**
    * @ORM\Column(type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="AUTO")
     */
    private $id;

    /**
     * @ORM\Column(type="string", nullable=True)
     */
    private $fitbitId;

    /**
     * @ORM\Column(type="text", nullable=True)
     */
    private $token;

    /**
     * @ORM\Column(type="text", nullable=True)
     */
    private $refreshToken;

    /**
     * @ORM\Column(type="string", nullable=True)
     */
    private $expires;

    /**
     * @ORM\Column(type="string", nullable=True)
     */
    private $hasExpired;

    /**
     * @ORM\OneToOne(targetEntity="Account", inversedBy="fitbit")
     */
     private $account;

    /**
     * Get id
     *
     * @return integer
     */
    public function getId()
    {
        return $this->id;
    }

    /**
     * Set token
     *
     * @param string $token
     *
     * @return Fitbit
     */
    public function setToken($token)
    {
        $this->token = $token;

        return $this;
    }

    /**
     * Get token
     *
     * @return string
     */
    public function getToken()
    {
        return $this->token;
    }

    /**
     * Set account
     *
     * @param \AppBundle\Entity\Account $account
     *
     * @return Fitbit
     */
    public function setAccount(\AppBundle\Entity\Account $account = null)
    {
        $this->account = $account;

        return $this;
    }

    /**
     * Get account
     *
     * @return \AppBundle\Entity\Account
     */
    public function getAccount()
    {
        return $this->account;
    }

    /**
     * Set refreshToken
     *
     * @param string $refreshToken
     *
     * @return Fitbit
     */
    public function setRefreshToken($refreshToken)
    {
        $this->refreshToken = $refreshToken;

        return $this;
    }

    /**
     * Get refreshToken
     *
     * @return string
     */
    public function getRefreshToken()
    {
        return $this->refreshToken;
    }

    /**
     * Set expires
     *
     * @param string $expires
     *
     * @return Fitbit
     */
    public function setExpires($expires)
    {
        $this->expires = $expires;

        return $this;
    }

    /**
     * Get expires
     *
     * @return string
     */
    public function getExpires()
    {
        return $this->expires;
    }

    /**
     * Set hasExpired
     *
     * @param string $hasExpired
     *
     * @return Fitbit
     */
    public function setHasExpired($hasExpired)
    {
        $this->hasExpired = $hasExpired;

        return $this;
    }

    /**
     * Get hasExpired
     *
     * @return string
     */
    public function hasExpired()
    {
        return $this->hasExpired;
    }

    public function __toString()
    {
      return $this->token;
    }

    /**
     * Set fitbitId
     *
     * @param string $fitbitId
     *
     * @return Fitbit
     */
    public function setFitbitId($fitbitId)
    {
        $this->fitbitId = $fitbitId;

        return $this;
    }

    /**
     * Get fitbitId
     *
     * @return string
     */
    public function getFitbitId()
    {
        return $this->fitbitId;
    }

    /**
     * Get hasExpired
     *
     * @return string
     */
    public function getHasExpired()
    {
        return $this->hasExpired;
    }
}
